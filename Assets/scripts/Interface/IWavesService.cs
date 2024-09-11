using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IWavesService
{
    Task<string> GetWavesById(int id);

    Task<string> GetWaves();

    Task<bool> PutWaves(int id, Waves model);
}

