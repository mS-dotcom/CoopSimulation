using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PomeloCase.LivingBeings
{
	public interface IAnimal
	{
		public string Gen { get; set; }
		public int Month { get; set; }
		
		
        
		
        public int OvulationTime { get; set; }
		


		IAnimal Ovulation();

    }
}