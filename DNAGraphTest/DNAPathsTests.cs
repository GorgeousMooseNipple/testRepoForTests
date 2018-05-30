using System;
using UKPO2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DNAGraphTest
{
    [TestClass]
    public class GraphUnitTest
    {
        //Проверяется правильность количества найденных путей
        [TestMethod]
        public void FoundPathsCountTest()
        {
            String molecule = "АГЦЦГГУААЦЦ";
            String[] fragmentsArray = { "АГЦЦ", "ЦГГУ", "ГГУАА", "УААЦЦ" };
            DNAGraph graph = new DNAGraph(molecule, fragmentsArray);
            List<List<String>> paths = graph.GetPaths();

            Assert.AreEqual(3, paths.Count);
        }

        //Проверяется ход выполнения при задании пустого графа
        [TestMethod]
        public void EmptyGraphTest()
        {
            String molecule = "АГЦЦГГУААЦЦ";
            String[] fragmentsArray = { };
            DNAGraph graph = new DNAGraph(molecule, fragmentsArray);
            var verticles = graph.VerticleList;

            Assert.AreEqual(0, verticles.Count);
        }

        //Одинаковые фрагменты молекулы
        [TestMethod]
        public void SameFragmentsTest()
        {
            String molecule = "АГЦЦАГЦЦАГЦЦ";
            String[] fragmentsArray = { "АГЦЦ", "АГЦЦ", "АГЦЦ" };
            DNAGraph graph = new DNAGraph(molecule, fragmentsArray);
            var verticles = graph.VerticleList;

            List<String> neighbours = new List<string>() { "АГЦЦ" };

            Assert.AreEqual(verticles[0].GetNeighbours(), neighbours);
            Assert.AreEqual(verticles[1].GetNeighbours(), neighbours);
            Assert.AreEqual(verticles[2].GetNeighbours(), new List<String>());
        }

        // Проверяется корректность найденных путей
        [TestMethod]
        public void PathFindingTest()
        {
            String molecule = "АГЦЦГГУААЦЦ";
            String[] fragmentsArray = { "АГЦЦ", "ЦГГУ", "ГГУАА", "УААЦЦ" };
            DNAGraph graph = new DNAGraph(molecule, fragmentsArray);
            List<List<String>> paths = graph.GetPaths();

            List<String> path1 = new List<String>() { "АГЦЦ", "ЦГГУ", "ГГУАА", "УААЦЦ" };
            List<String> path2 = new List<String>() { "АГЦЦ", "ЦГГУ", "УААЦЦ" };
            List<String> path3 = new List<String>() { "АГЦЦ", "ГГУАА", "УААЦЦ" };

            Assert.IsTrue(path1.SequenceEqual(paths[0]));
            Assert.IsTrue(path2.SequenceEqual(paths[1]));
            Assert.IsTrue(path3.SequenceEqual(paths[2]));
        }

        [TestMethod]
        public void GraphBuildingTest()
        {
            String molecule = "АГЦЦГГУАП";
            String[] fragmentsArray = { "АГЦЦ", "ЦГГУ", "ЦГГУ", "ГГУЦ", "ГУА" };
            DNAGraph graph = new DNAGraph(molecule, fragmentsArray);
            var verticlesList = graph.VerticleList;
            var verticlesNeighboursFragments = new List<List<String>>() {
                new List<String>() { "ЦГГУ" },
                new List<String>() { "ГУА" },
                new List<String>(),
                new List<String>()
               };

            for (var i = 0; i < verticlesList.Count; ++i)
            {
                var neighboursFragments = new List<String>();
                foreach (var neighbour in verticlesList[i].GetNeighbours())
                {
                    neighboursFragments.Add(neighbour.Fragment);
                }
                Assert.IsTrue(verticlesNeighboursFragments[i].SequenceEqual(neighboursFragments));
            }
        }
    }
}
