using Microsoft.AspNetCore.Mvc;
using ASHWorkersAPI.Models;
using ASHWorkersAPI.Data;
using System.Collections;

namespace ASHWorkersAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly ApiDbContext _dbContext;

    private readonly ILogger<WorkerController> _logger;

    public WorkerController(ApiDbContext dbContext, ILogger<WorkerController> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetAllWorkers")]
    public JsonResult GetAllWorkers()
    {
        
        try
        {
            var result = new ArrayList();
            var getAllManagers = _dbContext.Managers.Join(
              _dbContext.Workers,
              m => m.WorkerId,
              w => w.WorkerId,
              (m, w) => new
              {
                  Manager()
              });

            if (getAllManagers != null)
            {
                result.Add(getAllManagers);
            }

            var getAllSupervisors = _dbContext.Supervisors.Join(
              _dbContext.Workers,
              s => s.WorkerId,
              w => w.WorkerId,
              (s, w) => new
              {
                  Supervisor()
              });
            if (getAllSupervisors != null)
            {
                result.Add(getAllSupervisors);
            }

            var getAllEmployees = _dbContext.Employees.Join(
              _dbContext.Workers,
              e => e.WorkerId,
              w => w.WorkerId,
              (e, w) => new
              {
                  Employee()
              });
            if (getAllEmployees != null)
            {
                result.Add(getAllEmployees);
            }
            return new JsonResult(Ok(result));
        }
        catch
        {
            return new JsonResult("system ran into exception while doing DB operations: ");
        }
        

        
    }


    [HttpPost(Name = "AddWorker")]
    public int AddWorker(Worker worker,
        string workerType,
        double payPerHour = 0.0,
        double maxExpenseAllowed=0.0,
        double annualSalary = 0.0)
    {

        // Todo: Create Request Response Object to manipulate data rather than just model data

        //ToDo: Move to Enum
        var expectedWorkerType = new List<string>()
        {
            "manager",
            "employee",
            "supervisor"
        };

        if (!expectedWorkerType.Contains(workerType))
        {
            return new JsonResult(ArgumentException());
        }

        if(workerType.Equals("manager") && (maxExpenseAllowed == 0.0 || annualSalary==0.0)) {
            return new JsonResult(ArgumentException());
        }

        if (workerType.Equals("employee") && (payPerHour == 0.0))
        {
            return new JsonResult(ArgumentException());
        }

        if (workerType.Equals("supervisor") && (annualSalary == 0.0))
        {
            return new JsonResult(ArgumentException());
        }


        try
        {

            var w = new Worker {Address = worker.Address,
                FirstName = worker.FirstName,
                LastName = worker.LastName
            };
            _dbContext.AddObject(w);

            _dbContext.SaveChanges();

            w.WorkerId = worker.WorkerId;

            var e = new Employee { PayPerHour = payPerHour, Worker = w };

            if (workerType.Equals("employee"))
            {
                _dbContext.AddObject(e);

                _dbContext.SaveChanges();
            }

            var m = new Manager { MaxExpenseAmount = maxExpenseAllowed, AnnualSalary= annualSalary, Worker = w };
            if (workerType.Equals("manager"))
            {
                _dbContext.AddObject(m);

                _dbContext.SaveChanges();
            }
            var s = new Supervisor { AnnualSalary = annualSalary, Worker= w};
            if (workerType.Equals("supervisor"))
            {
                _dbContext.AddObject(s);

                _dbContext.SaveChanges();
            }

            return w.WorkerId;
        }
        catch
        {
            //ToDo: Rollback the last inserted Id in Worker
            return -1;
        }

    }
}

