Dating APP .Net 7 + Angular 14

Application allows to:
- Create new user by registration form
- Log in to given account
- Upload multiple of photos (Cloudinary cloud)
- Send messages to other users
- Give likes to other users
- Look through other people photos

Additionaly as admin
- you can moderate user roles

To run application go to:
- DatingApp-SPA folder and run(Node version 15+):
	- npm install
	- ng serve
	- Application will run on https://localhost:4200/
- DatingApp.API folder (.Net 7 runtinme and SDK required):
	- dotnet build
	- dotnet run --project .\DatingApp.API\
	- Server will be running on https://localhost:5001/
	
To log in as admin use
Username: admin Password: Pa$$w0rd

Other users to be found in .\DatingApp.API\DatingApp.API\Seed\UserData.json