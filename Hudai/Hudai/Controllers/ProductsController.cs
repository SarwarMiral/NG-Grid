using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Hudai.Models;

namespace Hudai.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Hudai.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Products>("Products");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductsController : ODataController
    {
        private DBContext db = new DBContext();

        // GET: odata/Products
        [EnableQuery]
        public IQueryable<Products> GetProducts()
        {
            return db.products;
        }

        // GET: odata/Products(5)
        [EnableQuery]
        public SingleResult<Products> GetProducts([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.products.Where(products => products.ID == key));
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri] Guid key, Delta<Products> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Products products = db.products.Find(key);
            if (products == null)
            {
                return NotFound();
            }

            patch.Put(products);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(products);
        }

        // POST: odata/Products
        public IHttpActionResult Post(Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.products.Add(products);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductsExists(products.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(products);
        }

        // PATCH: odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<Products> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Products products = db.products.Find(key);
            if (products == null)
            {
                return NotFound();
            }

            patch.Patch(products);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(products);
        }

        // DELETE: odata/Products(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            Products products = db.products.Find(key);
            if (products == null)
            {
                return NotFound();
            }

            db.products.Remove(products);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsExists(Guid key)
        {
            return db.products.Count(e => e.ID == key) > 0;
        }
    }
}
