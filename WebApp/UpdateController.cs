﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApp
{
    public class UpdateController : ApiController
    {
        public HttpResponseMessage Post([FromUri] string id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            switch (id)
            {
                case "alerts.bin":
                case "tripupdates.bin":
                case "vehiclepositions.bin":
                    {
                        var task = this.Request.Content.ReadAsStreamAsync();
                        task.Wait();
                        Stream requestStream = task.Result;
                        try
                        {
                            Stream fileStream = File.Create(HttpContext.Current.Server.MapPath("~/test/" + id));
                            requestStream.CopyTo(fileStream);
                            fileStream.Close();
                            requestStream.Close();
                            response.StatusCode = HttpStatusCode.Created;
                            return response;
                        }
                        catch (IOException)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                    }
                    break;
                default:
                    break;
            }
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }
        /*
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        */
    }
}