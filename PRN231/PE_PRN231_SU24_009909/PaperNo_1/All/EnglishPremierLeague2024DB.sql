USE master
GO

CREATE DATABASE EnglishPremierLeague2024DB
GO

USE EnglishPremierLeague2024DB
GO

CREATE TABLE PremierLeagueAccount (
  AccID int primary key,
  Password nvarchar(90) not null,
  EmailAddress nvarchar(90) unique, 
  Description nvarchar(140) not null,
  Role int
)
GO

INSERT INTO PremierLeagueAccount VALUES(551 ,N'@1','admin@EnglishPremierLeague.com', N'System Admin', 1);
INSERT INTO PremierLeagueAccount VALUES(552 ,N'@1','staff@EnglishPremierLeague.com', N'Staff', 2);
INSERT INTO PremierLeagueAccount VALUES(553 ,N'@1','manager@EnglishPremierLeague.com', N'Manager', 3);
INSERT INTO PremierLeagueAccount VALUES(554 ,N'@1','member1@EnglishPremierLeague.com', N'Member', 4);
GO

CREATE TABLE FootballClub (
  FootballClubID nvarchar(30) primary key,
  ClubName nvarchar(100) not null,
  ClubShortDescription nvarchar(400) not null, 
  SoccerPracticeField nvarchar(250) not null, 
  Mascos nvarchar(100) not null
)
GO

INSERT INTO FootballClub VALUES(N'CL004466', N'Chelsea FC', N'Chelsea FC is a professional football club based in Fulham, London. The club is known for its rich history, top-tier performances, and a strong presence in English football.', N'The club s official training ground is located in Cobham, Surrey, known as the Cobham Training Centre', N'The official mascot of Chelsea FC is Stamford the Lion.')
GO
INSERT INTO FootballClub VALUES(N'CL004467', N'Manchester City FC', N'Manchester City FC, based in Manchester, is a prominent club with a successful track record in English football. The club is known for its modern infrastructure and strong squad.', N'The Etihad Campus is home to Manchester City s state-of-the-art training facilities.', N'Moonchester and Moonbeam are the official club mascots of Manchester City FC.')
GO
INSERT INTO FootballClub VALUES(N'CL004468', N'Liverpool FC', N'Liverpool FC, based in Liverpool, is a historic club with a global fanbase. It has a rich footballing tradition and is known for its passionate supporters.', N'The AXA Training Centre serves as Liverpool FC s training facility.', N'The official club mascot of Liverpool FC is Mighty Red.')
GO
INSERT INTO FootballClub VALUES(N'CL004469', N'Manchester United FC', N'Manchester United FC, one of the most successful clubs in English football, is based in Greater Manchester. It has a storied history and is renowned for its achievements.', N'The AON Training Complex is the primary training ground for the club.', N'Fred the Red is the official mascot of Manchester United FC.')
GO
INSERT INTO FootballClub VALUES(N'CL004465', N'Tottenham Hotspur FC', N'Tottenham Hotspur FC, based in London, has a strong legacy and a reputation for attractive football. The club has a significant following and a distinguished past.', N'The Tottenham Hotspur Training Centre serves as the club s training base.', N'Chirpy the Cockerel is the official mascot of Tottenham Hotspur FC.')
GO



CREATE TABLE FootballPlayer (
  FootballPlayerID nvarchar(30) PRIMARY KEY,
  FullName nvarchar(100) not null,
  Achievements nvarchar(400) not null, 
  Birthday Datetime,
  PlayerExperiences nvarchar(400) not null, 
  Nomination nvarchar(400) not null, 
  FootballClubID nvarchar(30) FOREIGN KEY references FootballClub(FootballClubID) on delete cascade on update cascade,
)
GO


INSERT INTO FootballPlayer VALUES(N'PL00960', N'Vincent Kompany', N'Vincent Kompany was a cornerstone of Manchester City s defense, leading the team to multiple titles during his tenure.', CAST(N'1976-1-1' AS DateTime), N'Kompany s leadership and dedication made him a revered figure among fans and players at Manchester City.', N'Kompany is remembered for his strong presence on the field and his contributions to Manchester City s success.', N'CL004467')
GO
INSERT INTO FootballPlayer VALUES(N'PL00961', N'Raheem Sterling', N'Raheem Sterling has been a key player for Manchester City, showcasing his skills and versatility on the field.', CAST(N'1995-1-1' AS DateTime), N'Sterling s journey at Manchester City has been characterized by growth and consistent performances.', N'Sterling has been recognized for his impact on the team and his commitment to the club.', N'CL004467')
GO
INSERT INTO FootballPlayer VALUES(N'PL00962', N'Sergio Agüero', N'Sergio Agüero is a legendary figure at Manchester City, having scored crucial goals and played a pivotal role in the club s success.', CAST(N'1986-1-1' AS DateTime), N'Agüero s tenure at Manchester City has been marked by remarkable goal-scoring feats and moments of brilliance.', N'Agüero is highly regarded for his contributions to Manchester City s history.', N'CL004467')
GO
INSERT INTO FootballPlayer VALUES(N'PL00963', N'Kevin De Bruyne', N'Kevin De Bruyne is a highly decorated player, known for his instrumental role in Manchester City s triumphs in the Premier League and other tournaments.', CAST(N'1990-1-1' AS DateTime), N'De Bruyne s experience and skill have made him a vital part of the Manchester City squad.', N'De Bruyne has received numerous accolades for his outstanding performances on the field.', N'CL004467')
GO
INSERT INTO FootballPlayer VALUES(N'PL00964', N'Phil Foden', N'Phil Foden has been a standout player for Manchester City, contributing significantly to their successes in various competitions.', CAST(N'1989-1-1' AS DateTime), N'Foden has risen through the ranks of Manchester City s Academy to become a key player in the first team.', N'Foden has been recognized for his exceptional talent and dedication to the club.', N'CL004467')
GO
INSERT INTO FootballPlayer VALUES(N'PL00965', N'John Terr', N'John Terry, a Chelsea legend, enjoyed a successful career with the club, securing numerous Premier League titles, FA Cups, and a UEFA Champions League triumph, while also earning individual honors.', CAST(N'1980-12-7' AS DateTime), N'Terry s unwavering commitment to Chelsea, exceptional defensive skills, and influential captaincy established him as a revered figure and a symbol of resilience.', N'Terry received recognition for his impressive performances, including multiple titles as Chelsea Player of the Year and being named in the PFA Team of the Year.', N'CL004466')
GO
INSERT INTO FootballPlayer VALUES(N'PL00966', N'Didier Drogba', N'Didier Drogba played a pivotal role in Chelsea s success, contributing to the team s Premier League titles, FA Cups, and most notably, the UEFA Champions League victory in 2011-2012.', CAST(N'1978-3-11' AS DateTime), N'Drogba is renowned for his exceptional goal-scoring prowess, physical presence, and influential leadership, making him a fan favorite and a symbol of success at Chelsea.', N'Drogba garnered widespread recognition for his significant impact on Chelsea s success, earning multiple individual accolades throughout his career.', N'CL004466')
GO
INSERT INTO FootballPlayer VALUES(N'PL00967', N'Frank Lampard', N'Frank Lampard is a Chelsea legend, winning multiple Premier League titles, FA Cups, a UEFA Champions League, and other trophies during his distinguished career with the club.', CAST(N'1978-6-20' AS DateTime), N'With over a decade of service to Chelsea, Lampard is celebrated for his exceptional goal-scoring ability, remarkable work ethic, and influential leadership both on and off the pitch.', N'Lampard received numerous accolades, including being named Chelsea Player of the Year and earning a place in the Premier League Hall of Fame', N'CL004466')
GO
