namespace MusicR8r.Data
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MusicR8r.Data.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        const string AdministratorUserName = "info@telerikacademy.com";
        const string AdministratorPassword = "123456";



        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false) {
            //    System.Diagnostics.Debugger.Launch();
            //}


            string artistName = "Matt Heafy";
            string artistCountry = "Burgas";
            string artistBio = string.Format("I am " + artistName + " and I am from " + artistCountry);

            string albumName = "A sample album";

            string songName = "A sample song";
            int songDuration = 120;

            string genreName = "MyGenre";

            string userFirstName = "Ivan";
            string userLastName = "Georgiev";
            string userPhone = "08947474747";
            string userBio = "this is a sample user bio";

            var albums = new List<Album>();
            var songs = new List<Song>();

            var sampleGenre = new Genre { Name = genreName };


            var roleName = "Admin";

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = roleName };
            roleManager.Create(role);


            for (int i = 0; i < 100; i++)
            {
                var user = this.SeedUser(i, context, roleName, userFirstName + i, userLastName + i, userPhone, userBio + i);
                var artist = this.SeedArtist(context, artistName + i, artistCountry + i, artistBio + i);
                var album = this.SeedAlbum(context, albumName + i, artist, new List<User>() { user });
                var genre = this.SeedGenre(context, genreName + i);
                this.SeedSong(context, songName + i, artist, album, new List<Genre>() { genre }, songDuration + i);
            }
        }

        private Genre SeedGenre(MsSqlDbContext context, string name)
        {
            var genre = new Genre { Name = name };

            context.Genres.AddOrUpdate(genre);

            return genre;
        }

        private void SeedSong(MsSqlDbContext context, string name, Artist artist, Album album, ICollection<Genre> genres, int duration)
        {
            context.Songs.AddOrUpdate(new Song
            {
                Name = name,
                Duration = duration,
                Artist = artist,
                Album = album,
                Genres = genres,
            });
        }

        private Album SeedAlbum(MsSqlDbContext context, string name, Artist artist, ICollection<User> users)
        {
            var album = new Album
            {
                Name = name,
                Year = 1986,
                Artist = artist,
                Users = users
            };
            context.Albums.AddOrUpdate(album);

            return album;
        }

        private Artist SeedArtist(MsSqlDbContext context, string name, string countryOfOrigin, string bio)
        {
            var artist = new Artist
            {
                Name = name,
                CountryOfOrigin = countryOfOrigin,
                Bio = bio
            };

            context.Artists.AddOrUpdate(artist);
            return artist;
        }

        private User SeedUser(int index, MsSqlDbContext context, String roleName, string firstName, string lastName, string phone, string bio)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var user = new User
            {
                UserName = String.Format("{0}{1}", index, AdministratorUserName),
                Email = String.Format("{0}{1}", AdministratorUserName, index),
                EmailConfirmed = true,
                CreatedOn = DateTime.Now,
                FirstName = "first name",
                LastName = "last name",
                Bio = "bio"
            };
            var result = userManager.Create(user, AdministratorPassword);
            userManager.AddToRole(user.Id, roleName);

            return user;
        }
    }
}
