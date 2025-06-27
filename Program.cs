// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SimulationApp {
    using System;
    using System.Xml;
    using SimulationApp.Core.Domain.Shared;
    using SimulationApp.Core.Domain.Warehouses;
    using SimulationApp.Core.Infrastructure.Xml;
    using SimulationApp.Core.Models;
    using SimulationApp.Core.Models.Domain;
    using SimulationApp.Core.Models.Utils;

    public static class Program
    {
        public static int Strategy { get; set; } = 0;

        public static string ConfigPath { get; set; } = "./src/Ressources/configurations/config1.xml";

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting simulation test...");

            EnvironmentLoader simulationLoader = new EnvironmentLoader(new XmlReaderService(ConfigPath));
            EnvironmentModel simulationModel = simulationLoader.Load();
            SimulationLoop simulationLoop = new SimulationLoop(simulationModel.Buildings);

            // simulationLoop.RunAsync();
        }
    }
}
