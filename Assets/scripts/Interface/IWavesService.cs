using System.Threading.Tasks;
public interface IWavesService
{
    Task<string> GetWaves(int userId);

    Task<string> GetWaveStartPos();
}

