using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using WebApplication9.Areas.Admin.ViewModels;
using WebApplication9.Contexts;
using WebApplication9.Models;
using WebApplication9.ViewModels;

namespace WebApplication9.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class VillaController : Controller
{
    AppDbContext _context;
    IWebHostEnvironment _environment;

    public VillaController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Villa> villaList =await _context.Villas.ToListAsync(); 

        return View(villaList);
    }



    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(VillaVM villaVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("error", "yanlis");
            return View(villaVM);
        }
    
        if (villaVM.formFile.Length>2500000)
        {
            ModelState.AddModelError("img", "file is big");
            return View(villaVM);
        }

        if(!villaVM.formFile.ContentType.Contains("image"))
        {
            return View(villaVM);
        }

        string fileName= Path.GetFileName(villaVM.formFile.FileName);

        if (fileName.Length > 100)
        {
            fileName = Guid.NewGuid().ToString() + fileName.Substring(fileName.Length-64);
        }
        else
        {
            fileName = Guid.NewGuid().ToString()+fileName;
        }


        string path = Path.Combine(_environment.WebRootPath.ToString()+"\\images\\", fileName);
        using (FileStream stream=new FileStream(path,FileMode.Create))
        {
            villaVM.formFile.CopyTo(stream);
        }

        Villa villa = new Villa()
        {
            Name = villaVM.Name,
            Description = villaVM.Description,
            Category = villaVM.Category,
            ImgUrl=fileName,

        };

       await _context.AddAsync(villa);
       await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public IActionResult Delete(int id)
    {
        Villa a=_context.Villas.FirstOrDefault(x => x.Id == id);
        _context.Villas.Remove(a);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Update(int id)
    {
        Villa a= _context.Villas.FirstOrDefault(x=>x.Id == id);

        VillaVM villaVM = new VillaVM()
        {
            Id = id,
            Name = a.Name,
            Description = a.Description,
            Category=a.Category,
            ImgUrl=a.ImgUrl,
        };
        return View(villaVM);
    }
    [HttpPost]
    public async Task<IActionResult> Update(VillaVM villaVM)
    {
        if (villaVM.formFile != null)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "yanlis");
                return View(villaVM);
            }

            if (villaVM.formFile.Length > 2500000)
            {
                ModelState.AddModelError("img", "file is big");
                return View(villaVM);
            }

            if (!villaVM.formFile.ContentType.Contains("image"))
            {
                return View(villaVM);
            }

            string fileName = Path.GetFileName(villaVM.formFile.FileName);

            if (fileName.Length > 100)
            {
                fileName = Guid.NewGuid().ToString() + fileName.Substring(fileName.Length - 64);
            }
            else
            {
                fileName = Guid.NewGuid().ToString() + fileName;
            }


            string path = Path.Combine(_environment.WebRootPath.ToString() + "\\images\\", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                villaVM.formFile.CopyTo(stream);
            }
            


            Villa villa= _context.Villas.FirstOrDefault(x=>x.Id == villaVM.Id);

   

            villa.Name = villaVM.Name;
            villa.Category = villaVM.Category;
            villa.Description= villaVM.Description;
            villa.ImgUrl = path;

            _context.Villas.Update(villa);
            await _context.SaveChangesAsync();

            return Redirect("Index");
        }
        else
        {
            Villa villa = _context.Villas.FirstOrDefault(x => x.Id == villaVM.Id);

            villa.Name = villaVM.Name;
            villa.Category = villaVM.Category;
            villa.Description = villaVM.Description;
            villa.ImgUrl = villaVM.ImgUrl;

            _context.Villas.Update(villa);
            await _context.SaveChangesAsync();

            return Redirect("Index");
        }
    }

}
