/* 
 * Cineast RESTful API
 *
 * Cineast is vitrivr's content-based multimedia retrieval engine. This is it's RESTful API.
 *
 * The version of the OpenAPI document: v1
 * Contact: contact@vitrivr.org
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using Org.Vitrivr.CineastApi.Client;
using Org.Vitrivr.CineastApi.Api;
using Org.Vitrivr.CineastApi.Model;

namespace Org.Vitrivr.CineastApi.Test
{
    /// <summary>
    ///  Class for testing MetadataApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class MetadataApiTests
    {
        private MetadataApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new MetadataApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of MetadataApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOf' MetadataApi
            //Assert.IsInstanceOf(typeof(MetadataApi), instance);
        }

        
        /// <summary>
        /// Test FindMetaById
        /// </summary>
        [Test]
        public void FindMetaByIdTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //var response = instance.FindMetaById(id);
            //Assert.IsInstanceOf(typeof(MediaObjectMetadataQueryResult), response, "response is MediaObjectMetadataQueryResult");
        }
        
        /// <summary>
        /// Test FindMetaFullyQualified
        /// </summary>
        [Test]
        public void FindMetaFullyQualifiedTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //string domain = null;
            //string key = null;
            //var response = instance.FindMetaFullyQualified(id, domain, key);
            //Assert.IsInstanceOf(typeof(MediaObjectMetadataQueryResult), response, "response is MediaObjectMetadataQueryResult");
        }
        
        /// <summary>
        /// Test FindMetadataByDomain
        /// </summary>
        [Test]
        public void FindMetadataByDomainTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string domain = null;
            //string id = null;
            //var response = instance.FindMetadataByDomain(domain, id);
            //Assert.IsInstanceOf(typeof(MediaObjectMetadataQueryResult), response, "response is MediaObjectMetadataQueryResult");
        }
        
        /// <summary>
        /// Test FindMetadataByDomainBatched
        /// </summary>
        [Test]
        public void FindMetadataByDomainBatchedTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string domain = null;
            //IdList idList = null;
            //var response = instance.FindMetadataByDomainBatched(domain, idList);
            //Assert.IsInstanceOf(typeof(MediaObjectMetadataQueryResult), response, "response is MediaObjectMetadataQueryResult");
        }
        
        /// <summary>
        /// Test FindMetadataByKey
        /// </summary>
        [Test]
        public void FindMetadataByKeyTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string key = null;
            //string id = null;
            //var response = instance.FindMetadataByKey(key, id);
            //Assert.IsInstanceOf(typeof(MediaObjectMetadataQueryResult), response, "response is MediaObjectMetadataQueryResult");
        }
        
        /// <summary>
        /// Test FindMetadataByKeyBatched
        /// </summary>
        [Test]
        public void FindMetadataByKeyBatchedTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string key = null;
            //IdList idList = null;
            //var response = instance.FindMetadataByKeyBatched(key, idList);
            //Assert.IsInstanceOf(typeof(MediaObjectMetadataQueryResult), response, "response is MediaObjectMetadataQueryResult");
        }
        
        /// <summary>
        /// Test FindMetadataForObjectIdBatched
        /// </summary>
        [Test]
        public void FindMetadataForObjectIdBatchedTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //OptionallyFilteredIdList optionallyFilteredIdList = null;
            //var response = instance.FindMetadataForObjectIdBatched(optionallyFilteredIdList);
            //Assert.IsInstanceOf(typeof(MediaObjectMetadataQueryResult), response, "response is MediaObjectMetadataQueryResult");
        }
        
    }

}
