using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Game
	{
		public int GameId { get; set; }
		public int UserId { get; set; }
		public DateTime Date { get; set; }
		public bool Win { get; set; }
	}
}
