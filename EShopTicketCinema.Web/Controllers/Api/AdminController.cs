using EShopTicketCinema.Domain.DomainModels;
using EShopTicketCinema.Domain.Identity;
using EShopTicketCinema.Services.Interface;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EShopTicketCinema.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<TicketCinemaUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(IOrderService orderService, UserManager<TicketCinemaUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._orderService = orderService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetOrders()
        {
            return this._orderService.getAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForTicket(BaseEntity model)
        {
            return this._orderService.getOrderDetails(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportAllUsers(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }

            List<TicketCinemaUser> users = getUsersFromExcelFile(file.FileName);

            bool status = true;
            string[] roleNames = { "Regular" };

            foreach (var item in users)
            {
                var userCheck = userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var user = new TicketCinemaUser
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        Password = item.Password,
                        Role = item.Role,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = item.PhoneNumber,
                        UserCart = new ShoppingCart()
                    };

                    var roleExist = await roleManager.RoleExistsAsync(item.Role);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(item.Role));
                    }

                    var result = userManager.CreateAsync(user, item.Password).Result;
                    await userManager.AddToRoleAsync(user, item.Role);

                    status = status & result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("Index", "Order");
        }

        private List<TicketCinemaUser> getUsersFromExcelFile(string fileName)
        {
            string pathToFile = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<TicketCinemaUser> userList = new List<TicketCinemaUser>();

            using (var stream = System.IO.File.Open(pathToFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        userList.Add(new TicketCinemaUser
                        {
                            Email = reader.GetValue(0).ToString(),
                            Password = reader.GetValue(1).ToString(),
                            Role = reader.GetValue(2).ToString()
                        });
                    }
                }
            }
            return userList;

        }

    }
}
