using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ALG.Data.Entities;
using ALG.Data.Services;
using ALG.Web.Models;

namespace ALG.Web.Controllers;

public class AlgController : BaseController
{
    private IAlgorithmService svc;

    public AlgController()
    {
        svc = new AlgorithmService();
    }//controller 

    public IActionResult Index(AlgViewModel search)
    {
        search.Algorithms = svc.SearchAlgorithms(search.Query);
        return View(search);
    }//get algorithims


    public IActionResult Details(int id)
    {
        var algorithim = svc.GetAlgorithm(id);
       
        if (algorithim is null) {
            Alert("algorithim not found", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }
        return View(algorithim);
    }//

    public IActionResult Guide()
        {
            return View();
        }

}
