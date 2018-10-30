using Tranquillity.InMemory.Storage.Api.Common;
using Tranquillity.InMemory.Storage.Api.Models;
using Tranquillity.InMemory.Storage.Business.Contracts;
using Tranquillity.InMemory.Storage.ValueContracts;
using System;
using System.Web.Http;

namespace Tranquillity.InMemory.Storage.Api.Controllers
{
    [AllowAnonymous]
    public class KeyValueDataController : BaseApiController
    {
        /// <summary>
        /// keyValueStoreBusiness instance
        /// </summary>
        private readonly IStoreBusiness<Product> keyValueStoreBusiness;

        /// <summary>
        /// Initializes a new KeyValueDataController instance
        /// </summary>
        /// <param name="keyValueStoreBusiness"></param>
        public KeyValueDataController(IStoreBusiness<Product> keyValueStoreBusiness)
        {
            if (keyValueStoreBusiness == null)
                throw new ArgumentNullException("keyValueStoreBusiness");

            this.keyValueStoreBusiness = keyValueStoreBusiness;
        }

        /// <summary>
        /// Adds or Updates the store based on the input.
        /// </summary>
        /// <param name="keyValueDataModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(KeyValueDataModel keyValueDataModel)
        {
            try
            {
                if (!this.ModelState.IsValid || keyValueDataModel == null)
                {
                    return this.BadRequest(this.ModelState);
                }
                
                this.keyValueStoreBusiness.Put(keyValueDataModel.NameSpace, keyValueDataModel.Key, keyValueDataModel.Value);

                return this.CreateHttpResponse(Response.Success("Success"));
            }
            catch (Exception ex)
            {
                return this.CreateHttpResponse(Response.Failed(ex.Message));
            }
        }

        /// <summary>
        /// Gets a Key-Value item by namespace and key
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(string nameSpace, string key)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var response = this.keyValueStoreBusiness.Get(nameSpace, key);

                return this.CreateHttpResponse(Response.Success(response));
            }
            catch (Exception ex)
            {
                return this.CreateHttpResponse(Response.Failed(ex.Message));
            }
        }

        /// <summary>
        /// Deletes the specified key-value item.
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(string nameSpace, string key)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var response = this.keyValueStoreBusiness.Delete(nameSpace, key);

                return this.CreateHttpResponse(Response.Success(response));
            }
            catch (Exception ex)
            {
                return this.CreateHttpResponse(Response.Failed(ex.Message));
            }
        }

        /// <summary>
        /// Gets all Key-Value items by namespace
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Values(string nameSpace)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var response = this.keyValueStoreBusiness.Values(nameSpace);

                return this.CreateHttpResponse(Response.Success(response));
            }
            catch (Exception ex)
            {
                return this.CreateHttpResponse(Response.Failed(ex.Message));
            }
        }
    }
}