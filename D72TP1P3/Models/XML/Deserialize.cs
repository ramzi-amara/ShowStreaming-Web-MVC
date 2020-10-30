namespace D72TP1P3.Models.XML {
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using D72TP1P3.Models.DataModels;
    public static class Deserialize {
        public static void DeserializeTVShow(string path = "~/Content/Media/tvshows.xml") {
            string realPath = HttpContext.Current.Server.MapPath(path);
            string logPath = HttpContext.Current.Server.MapPath("~/App_Data/log.txt");
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(XMLTvShows));
            System.IO.StreamReader file = new System.IO.StreamReader(realPath);
            XMLTvShows XMLTvshows = (XMLTvShows)reader.Deserialize(file);
            TVShowDb TVShowDb = new TVShowDb();
            foreach (XMLTvShow XMLTvShow in XMLTvshows.Tvshow) {
                TvShow tvshow = new TvShow();
                tvshow.CopyPropertiesFrom(XMLTvShow);
                tvshow.TVParentalGuideline = EnumHelper<TvShow.TVParentalGuidelines>.GetValueFromName(XMLTvShow.Mpaa);
                Studio studio = TVShowDb.Studios.Where(s => s.Name == XMLTvShow.Studio).SingleOrDefault();
                if (studio == null) {
                    studio = new Studio { Name = XMLTvShow.Studio };
                    TVShowDb.Studios.Add(studio);
                }
                tvshow.Studio = studio;
                TVShowDb.TvShows.Add(tvshow);
                try {
                    TVShowDb.SaveChanges();
                }
                catch (Exception) {
                    System.Diagnostics.Debugger.Break();
                }
                foreach (string xmlgenre in XMLTvShow.Genres.Genre) {
                    if (TVShowDb.Genres.Where(q => q.Name == xmlgenre).SingleOrDefault() == null) {
                        TVShowDb.Genres.Add(new Genre { Name = xmlgenre });
                    }
                }
                try {
                    TVShowDb.SaveChanges();
                }
                catch (Exception) {
                    System.Diagnostics.Debugger.Break();
                }
                foreach (string xmlgenre in XMLTvShow.Genres.Genre) {
                    Genre g = TVShowDb.Genres.Where(q => q.Name == xmlgenre).Single();
                    tvshow.Genres.Add(g);
                }
                try {
                    TVShowDb.SaveChanges();
                }
                catch (Exception) {
                    System.Diagnostics.Debugger.Break();
                }
                foreach (XMLActor xmlactor in XMLTvShow.Actors.Actor) {
                    Actor actor = TVShowDb.Actors.Where(q => q.Name == xmlactor.Name).SingleOrDefault();
                    if (actor == null) {
                        actor = new Actor();
                        xmlactor.Name = Regex.Replace(xmlactor.Name, @"(.*?)\(", @"$1").Trim();
                        actor.CopyPropertiesFrom(xmlactor);
                        actor.Name = xmlactor.Name;
                        TVShowDb.Actors.Add(actor);
                    }
                    TVShowDb.Roles.Add(new Role { Actor = actor, TvShow = tvshow, Character = xmlactor.Role });
                    TVShowDb.SaveChanges();
                }
                try {
                    TVShowDb.SaveChanges();
                }
                catch (Exception) {
                    System.Diagnostics.Debugger.Break();
                }
                foreach (XMLSeason XMLSeason in XMLTvShow.Seasons.Season) {
                    Season season = tvshow.Seasons.Where(q => q.Number == XMLSeason.Number).SingleOrDefault();
                    if (season == null) {
                        season = new Season();
                        season.CopyPropertiesFrom(XMLSeason);
                        season.TvShow = tvshow;
                        tvshow.Seasons.Add(season);
                    }
                    foreach (XMLEpisode xmlepisode in XMLSeason.Episodes.Episode) {
                        Episode episode = new Episode();
                        episode.CopyPropertiesFrom(xmlepisode);
                        episode.Aired = DateTime.ParseExact(xmlepisode.Aired, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        episode.Season = season;
                        season.Episodes.Add(episode);
                        try {
                            TVShowDb.SaveChanges();
                        }
                        catch (Exception) {
                            System.Diagnostics.Debugger.Break();
                        }
                    }
                    try {
                        TVShowDb.SaveChanges();
                    }
                    catch (Exception) {
                        System.Diagnostics.Debugger.Break();
                    }
                }

            }
            TVShowDb.SaveChanges();
            file.Close();
        }
    }
}