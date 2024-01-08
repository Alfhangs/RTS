using NUnit.Framework;
using RTS.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTS.Test {
    public class ResourceDataTest : BaseTest
    {
        private static List<string> resources = new List<string>()
        {
            "Gold",
            "Food",
            "Wood"
        };

        [Test]
        public void ResourceDataTestWithResources([ValueSource("resources")] string resource)
        {
            ResourceData data = LoadResource(resource);

            Assert.IsNotNull(data, $"ResourceConfiguration {resource} not found.");
            Assert.IsTrue(data.ProductionLevel > 0, "ProductionLevel must be greater than 0.");
            Assert.IsTrue(data.ProductionPerSecond > 0, "ProductionPerSecond must be greater than 0.");
        }

        private ResourceData LoadResource(string resource)
        {
            return AssetDatabase.LoadAssetAtPath<ResourceData>($"Assets/Scriptables/Resources/{resource}.asset");
        }
    }
}