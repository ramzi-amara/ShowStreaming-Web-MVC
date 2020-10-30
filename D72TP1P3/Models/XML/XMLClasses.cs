namespace D72TP1P3.Models.XML {
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    [XmlRoot(ElementName = "genres")]
    public class XMLGenres {
        [XmlElement(ElementName = "genre")]
        public List<string> Genre { get; set; }

    }
    [XmlRoot(ElementName = "actor")]
    public class XMLActor {
        [XmlElement(ElementName = "actorName")]
        public string Name { get; set; }

        [XmlElement(ElementName = "actorRole")]
        public string Role { get; set; }

        [XmlElement(ElementName = "actorThumbUrl")]
        public string ThumbUrl { get; set; }

    }
    [XmlRoot(ElementName = "actors")]
    public class XMLActors {
        [XmlElement(ElementName = "actor")]
        public List<XMLActor> Actor { get; set; }

    }
    [XmlRoot(ElementName = "episode")]
    public class XMLEpisode {
        [XmlElement(ElementName = "episodeTitle")]
        public string Title { get; set; }

        [XmlElement(ElementName = "episodeNumber")]
        public byte Number { get; set; }

        [XmlElement(ElementName = "episodePlot")]
        public string Plot { get; set; }

        [XmlElement(ElementName = "episodeRuntime")]
        public int Runtime { get; set; }

        [XmlElement(ElementName = "episodeAired")]
        public string Aired { get; set; }

    }
    [XmlRoot(ElementName = "episodes")]
    public class XMLEpisodes {
        [XmlElement(ElementName = "episode")]
        public List<XMLEpisode> Episode { get; set; }

    }
    [XmlRoot(ElementName = "season")]
    public class XMLSeason {
        [XmlElement(ElementName = "SeasonNumber")]
        public byte Number { get; set; }

        [XmlElement(ElementName = "episodes")]
        public XMLEpisodes Episodes { get; set; }

    }
    [XmlRoot(ElementName = "seasons")]
    public class XMLSeasons {
        [XmlElement(ElementName = "season")]
        public List<XMLSeason> Season { get; set; }

    }
    [XmlRoot(ElementName = "tvshow")]
    public class XMLTvShow {
        [XmlElement(ElementName = "tvshowTitle")]
        public string Title { get; set; }

        [XmlElement(ElementName = "tvshowPlot")]
        public string Plot { get; set; }

        [XmlElement(ElementName = "tvshowRating")]
        public decimal Rating { get; set; }

        [XmlElement(ElementName = "tvshowYear")]
        public int Year { get; set; }

        [XmlElement(ElementName = "imdb")]
        public string IMDBId { get; set; }

        [XmlElement(ElementName = "zap2it")]
        public string Zap2it { get; set; }

        [XmlElement(ElementName = "tvdb")]
        public string TVDBId { get; set; }

        [XmlElement(ElementName = "tvshowStudio")]
        public string Studio { get; set; }

        [XmlElement(ElementName = "tvshowCertification")]
        public string Mpaa { get; set; }

        [XmlElement(ElementName = "genres")]
        public XMLGenres Genres { get; set; }

        [XmlElement(ElementName = "actors")]
        public XMLActors Actors { get; set; }

        [XmlElement(ElementName = "seasons")]
        public XMLSeasons Seasons { get; set; }

    }
    [XmlRoot(ElementName = "tvshows")]
    public class XMLTvShows {
        [XmlElement(ElementName = "tvshow")]
        public List<XMLTvShow> Tvshow { get; set; }

    }
}
