using System;
using System.Collections.Generic;
using ALG.Data.Entities;

namespace ALG.Data.Services;

public interface IAlgorithmService
{
    void Initialise();

    List<Algorithm> GetAlgorithms();
    Algorithm GetAlgorithm(int id);
    Algorithm AddAlgorithm (Algorithm a);
    Algorithm GetAlgorithmByName(string name);
    List <Algorithm> SearchAlgorithms(string searchString);
}