using Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Base
{
	/// <summary>
	/// Выполняет перенос данных из одного источника в другой по указанным маршрутам.
	/// </summary>
	public class PointToPointDataLoader : IDataLoader
	{
		public IDataReader Source { get; set; }

		public IReadOnlyCollection<ITrack> Tracks { get; set; }

		public IDataWriter Destination { get; set; }

		public void Dispose()
		{
			Source?.Dispose();

			Source = null;
			Destination = null;
		}

		public StringBuilder Run(bool validate)
		{
			Validate();

			return Destination.Write(Source, Tracks, validate: validate);
		}

		private void Validate()
		{
			if (Source == null)
				throw new InvalidOperationException("Не указан источник данных.");

			if (Destination == null)
				throw new InvalidOperationException("Не указан приемник данных.");

			if (Tracks == null || Tracks.Count == 0)
				throw new InvalidOperationException("Не указаны маршруты.");

			foreach (var track in Tracks)
			{
				if (track == null)
					throw new NullReferenceException("Маршрут не может быть null.");

				track.Validate();

				var dublicate = Tracks.FirstOrDefault(x =>
				{
					if (object.ReferenceEquals(x, track))
						return false;

					if (x.Source == null || track.Source == null)
						return x.Destination.Name == track.Destination.Name;

					return x.Destination.Name == track.Destination.Name;
				});

				if (dublicate != null)
					throw new InvalidOperationException($"Дубликаты имен полей в приемнике недопустимы. Маршрут [{dublicate.Source?.Name ?? "<не указано>"} - {dublicate.Destination}] пересакается с маршрутом [{track.Source?.Name ?? "<не указано>"} - {track.Destination}]");
			}
		}
	}
}
