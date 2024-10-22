public class Waves
{
    public int id;

    public int userID;

    public int wavesNumber;

    public int durationInSeconds;

    public int wavesPower;

    public Waves (int id, int userID, int wavesNumber, int durationInSeconds, int wavesPower) 
    { 
        this.id = id;
        this.userID = userID;
        this.wavesNumber = wavesNumber;
        this.durationInSeconds = durationInSeconds;
        this.wavesPower = wavesPower;
    }
}
