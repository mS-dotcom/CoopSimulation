using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PomeloCase.LivingBeings;
using PomeloCase.Models;

namespace PomeloCase.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;
    private IMemoryCache _memoryCache;

    public HomeController(ILogger<HomeController> logger,IConfiguration config, IMemoryCache memoryCache)
    {
        _logger = logger;
        _config = config;
        _memoryCache = memoryCache;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Start(int loop)
    {
        

        _memoryCache.Set<int>("loop", loop);
        
        int startedCount = _config.GetValue<int>(
                "StartedAnimal");

        List<Rabbit> rabbits = new List<Rabbit>();
        
        for (int i = 0; i < startedCount; i++)
        {
            Rabbit rbt = new Rabbit();
            var animal = rbt.Ovulation();

            rbt.Gen = animal.Gen;
            rbt.Month = animal.Month;
            
            
            
            rbt.OvulationTime = animal.OvulationTime;


            rabbits.Add(rbt);
        }

        _memoryCache.Set<List<Rabbit>>("rabbits", rabbits);
        return Json(startedCount);
    }
    public IActionResult Ovulation()
    {
        List<Rabbit> MainRabbits = _memoryCache.Get<List<Rabbit>>("rabbits");
        List<Rabbit> OvulationRabbits = _memoryCache.Get<List<Rabbit>>("rabbits");
        
        List<Rabbit> maleRabbits = MainRabbits.Where(x => x.Gen == "male").ToList();
        List<Rabbit> femaleRabbits = MainRabbits.Where(x => x.Gen == "female").ToList();

        
        int firstCount = MainRabbits.Count;
        int count = MainRabbits.Count;
        for (int i = 0; i < count; i++)
        {

            if (MainRabbits[i].Month > 3 && MainRabbits[i].Month >= MainRabbits[i].OvulationTime)
            {
                if (MainRabbits[i].Gen == "female")
                {
                    for (int y = 0; y < maleRabbits.Count; y++)
                    {
                        var x = MainRabbits[i];
                        femaleRabbits.Remove(x);


                        Rabbit rbt = new Rabbit();
                        var animal = rbt.Ovulation();

                        rbt.Gen = animal.Gen;
                        rbt.Month = animal.Month;
                        rbt.OvulationTime = animal.OvulationTime;
                        MainRabbits.Add(rbt);
                    }
                    

                }
            }
            else
            {
                MainRabbits[i].Month++;
            }
        }
        _memoryCache.Remove("rabbits");
        _memoryCache.Set<List<Rabbit>>("rabbits", MainRabbits);
        int record = MainRabbits.Count - firstCount;
        return Json(record);


    }
    public IActionResult DeathControl()
    {
        List<Rabbit> MainRabbits = _memoryCache.Get<List<Rabbit>>("rabbits");
        int firstCount = MainRabbits.Count;

        Random rnd = new Random();
        
        for (int i = 0; i < MainRabbits.Count; i++)
        {
            int RandomDeathMounth = rnd.Next(3, 10);
            if (MainRabbits[i].Month > RandomDeathMounth)
            {
                MainRabbits.Remove(MainRabbits[i]);
            }
        }
        int lastCount = firstCount - MainRabbits.Count;
        _memoryCache.Remove("rabbits");
        _memoryCache.Set<List<Rabbit>>("rabbits", MainRabbits);
        return Json(lastCount);

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

