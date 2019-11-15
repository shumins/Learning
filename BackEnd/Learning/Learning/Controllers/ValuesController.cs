using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.Data;
using Learning.IRepository;
using Learning.IService;
using Microsoft.AspNetCore.Mvc;

namespace Learning.Controllers
{
    


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly IUserRepository _user;
        private readonly EFCoreContext _context;
        public ValuesController(IUserServices userServices, EFCoreContext context, IUserRepository userRepository, IRoleServices roleServices) {
            _userServices = userServices;
            _context = context;
            _user = userRepository;
            _roleServices = roleServices;

        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {   
            //直接代用EF
            var b = _context.User.SingleOrDefault(x=>x.Id==1);
            //仓储层调用  后续会废弃 从Services调用
            var d = _user.Get(1);
            var c = _user.Get(x => x.Id == 1);
            //Services层调用，推荐
            var a = _userServices.Get(x => x.Id == 1);
         

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    
    }
}
