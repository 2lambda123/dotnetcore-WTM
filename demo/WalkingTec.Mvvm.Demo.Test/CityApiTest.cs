﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Vue3Demo.Controllers;
using WalkingTec.Mvvm.Vue3Demo.SchoolData.ViewModels.CityVMs;
using WalkingTec.Mvvm.ReactDemo.Models;
using WalkingTec.Mvvm.Vue3Demo;


namespace WalkingTec.Mvvm.Vue3Demo.Test
{
    [TestClass]
    public class CityApiTest
    {
        private CityController _controller;
        private string _seed;

        public CityApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<CityController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new CitySearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            CityVM vm = _controller.Wtm.CreateVM<CityVM>();
            City v = new City();
            
            v.Name = "T3DvxRSItVLx63PZnS";
            v.Level = 57;
            v.ParentId = AddCity();
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<City>().Find(v.ID);
                
                Assert.AreEqual(data.Name, "T3DvxRSItVLx63PZnS");
                Assert.AreEqual(data.Level, 57);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            City v = new City();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "T3DvxRSItVLx63PZnS";
                v.Level = 57;
                v.ParentId = AddCity();
                context.Set<City>().Add(v);
                context.SaveChanges();
            }

            CityVM vm = _controller.Wtm.CreateVM<CityVM>();
            var oldID = v.ID;
            v = new City();
            v.ID = oldID;
       		
            v.Name = "DqlE9W1uTfR";
            v.Level = 69;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Level", "");
            vm.FC.Add("Entity.ParentId", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<City>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "DqlE9W1uTfR");
                Assert.AreEqual(data.Level, 69);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            City v = new City();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "T3DvxRSItVLx63PZnS";
                v.Level = 57;
                v.ParentId = AddCity();
                context.Set<City>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            City v1 = new City();
            City v2 = new City();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "T3DvxRSItVLx63PZnS";
                v1.Level = 57;
                v1.ParentId = AddCity();
                v2.Name = "DqlE9W1uTfR";
                v2.Level = 69;
                v2.ParentId = v1.ParentId; 
                context.Set<City>().Add(v1);
                context.Set<City>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<City>().Find(v1.ID);
                var data2 = context.Set<City>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddCity()
        {
            City v = new City();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "IOYxFn";
                v.Level = 73;
                context.Set<City>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
