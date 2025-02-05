using Assets.scripts.Interface.Models;
using System.Threading.Tasks;
public interface IWavesService
{
    Task<StartWavePosition> GetWaveStartPos();

    Task<Waves> GetWave();

    Task<bool> PostWaveStatus(WaveStatusModel model);
}

