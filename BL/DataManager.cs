using BuissnesLayaer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayaer
{
	public class DataManager
    {
		private IUserRep _userRep;
		private IGameRep _gameRep;

		public DataManager(IUserRep userRep, IGameRep gameRep)
		{
			_userRep = userRep;
			_gameRep = gameRep;
		}

		public IUserRep Users { get { return _userRep; } }
		public IGameRep Games { get { return _gameRep; } }
	}
}
