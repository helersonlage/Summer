using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Summer.Dbcontex;
using Summer.Model;

namespace Summer.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {

        private SummerContext db = new SummerContext();


        [HttpGet]
        [Route("", Name = "Get")]
        public IQueryable<Client> GetAll()
        {
            return db.Clients.OrderBy(n => n.FirstName).ThenBy(n => n.LastName);
        }


        [HttpGet]
        [Route("api/[controller]/{id}/GetByID", Name = "GetByID")]
        public IQueryable<Client> Get(int id)
        {
            return db.Clients.Where(n => n.ClientID == id);
        }

        [HttpPost]
        [Route("")]
        public IActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ClientID }, client);
        }

        [HttpPut]
        [Route("", Name = "Put")]
        public IActionResult PutClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.ClientID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode((int)(HttpStatusCode.NoContent));
        }


        [HttpDelete]
        [Route("", Name = "Delete")]
        public IActionResult Delete(int id)
        {
            var client = db.Clients.Find(id);
            if (client?.ClientID > 0)
            {
                try
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return StatusCode((int)(HttpStatusCode.Conflict));
                }
                return Ok(client);

            }
            else
            {
                return NotFound();
            }


        }



        private bool ClientExists(int id)
        {
            return db.Clients.Any(n => n.ClientID == id);
        }




    }
}
