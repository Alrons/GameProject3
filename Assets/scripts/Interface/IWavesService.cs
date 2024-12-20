using System.Threading.Tasks;
public interface IWavesService
{
    Task<StartWavePosition> GetWaveStartPos();
}

