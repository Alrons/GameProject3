using System;

public class Waves
{
    public int Id { get; set; }

    public int UserID { get; set; }

    public int WavesNumber { get; set; }

    public int DurationInSeconds { get; set; }

    public int WavesPower { get; set; }

    public DateTime StartWave { get; set; }

    public double Passing { get; set; }

    public double WaveEnd { get; set; }

    public int WaveHealth { get; set; }

    public int Status { get; set; }

    public Waves (int id, int userID, int wavesNumber, int durationInSeconds, int wavesPower, DateTime startWave, double passing, double waveEnd, int waveHealth, int healthLevel1, int healthLevel2, int healthLevel3, int status) 
    { 
        Id = id;
        UserID = userID;
        WavesNumber = wavesNumber;
        DurationInSeconds = durationInSeconds;
        WavesPower = wavesPower;
        StartWave = startWave;
        Passing = passing;
        WaveEnd = waveEnd;
        WaveHealth = waveHealth;
        Status = status;
    }
}
