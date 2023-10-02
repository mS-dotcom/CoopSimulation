using System;
namespace PomeloCase.LivingBeings
{
    public class Rabbit : IAnimal
    {
        
        public int Month { get ; set ; }
        public int NumberOfBirthsThroughoutTheYear { get; set; }
        public int NumberOfBirthsThisYear { get; set; }
        
        public int OvulationTime { get; set; }
        public string Gen { get; set; }

        public IAnimal Ovulation()
        {
            Random rnd = new Random();
            Rabbit newRabbit = new Rabbit();

            int genderProbability = rnd.Next(0,100);
            switch (genderProbability)
            {
                case <50:newRabbit.Gen = "female";
                    break;
                case >50:newRabbit.Gen= "male";
                    break;
            }
            newRabbit.Month = rnd.Next(0, 6);
            
            
            
            
            newRabbit.OvulationTime = rnd.Next(4, 10);
            return newRabbit;
        }
    }
}

