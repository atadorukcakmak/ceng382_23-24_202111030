using System.Security.Claims;
using loginDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Namespace
{
    [Authorize]
    public class ToDoModel : PageModel
    {
        [BindProperty]

        public TblTodo NewToDo { get; set; } = default!;

        public UserToDoDatabaseContext ToDo= new();
        public List<TblTodo> ToDoList { get;set; } = default!;

        public void OnGet()
        {
            // LINQ query to retrieve items where IsDeleted is false
                    ToDoList = (from item in ToDo.TblTodos ///ToDoDB yerine bu var bnc
                                where item.IsDeleted == false
                                select item).ToList();
        }


        public IActionResult OnPostDelete(int id)
        {
           // var itemToUpdate = ToDoList.FirstOrDefault(item => item.Id == id);
            if (ToDo.TblTodos != null)
            {
                var todo = ToDo.TblTodos.Find(id);
                if (todo != null)
                {
                    todo.IsDeleted = true;
                    ToDo.SaveChanges();
                }
            }            

            return RedirectToAction("Get");
        }        

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewToDo == null)
            {
                return Page();
            }
            NewToDo.IsDeleted = false;
            ToDo.Add(NewToDo);
            ToDo.SaveChanges();
            return RedirectToAction("Get");
        }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public ToDoModel(IHttpContextAccessor httpContextAccessor) //constructor ismi değişti eskiden CustomerService idi
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<string> GetCustomers()
        {
            Console.WriteLine("User Id: " +_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine("Username: " + _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name));
            return new string[] { "John Doe", "Jane Doe" };
        }


    }
}
  