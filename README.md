# set up docker desktop 

For developer, setup in local PC, one cmd: "docker-compose up --build" to bring all redis, mongodb, mysql to use in prj

				https://docs.docker.com/get-docker/

# dockerfile

				eg: this git
				D:/.../WebApiTemplate
				D:/.../WebApiTemplate.sln
				D:/.../Dockerfile
				D:/.../docker-compose.yml

				
copy files: Dockerfile

can change "WebApiTemplate" (in dockerfile) into your directory
				
				eg:
				your directory prj:
				D:/TestApi/webapi #folder asp.net core prj
				D:/TestApi/webapi.sln
				# Docker file location should like that
				D:/TestApi/Dockerfile

				replace:
				"WebApiTemplate" into "webapi"

				WebApiTemplate.csproj into webapi.csproj 
				WebApiTemplate.dll into webapi.dll 
			

# docker compose

copy files: docker-compose.yml into your prj folder

				eg: this git
				D:/.../WebApiTemplate
				D:/.../WebApiTemplate.sln
				D:/.../Dockerfile
				D:/.../docker-compose.yml

can change "D:/dockersampleforaspdotnet" (in docker-compose.yml) this path by your prj dir copied docker-compose.yml

				Do your code in prj c# 
				eg: 
				D:/TestApi/webapi #folder asp.net core prj
				D:/TestApi/webapi.sln

				D:/TestApi/Dockerfile
				D:/TestApi/docker-compose.yml

				replace "D:/dockersampleforaspdotnet" (in docker-compose.yml) into "D:/TestApi"

open powershell or cmd in your proj folder
		
				eg:
				D:/TestApi/

				open cmd in D:/TestApi/ type command:
				
                docker-compose up --build

				Open dashboard of docker desktop

check file docker-compose.yml if you want to change default port / user/ pass

				redis connnect by: localhost:9379 pass: 123456
				mysql connnect by: localhost:9306 user: root ; pass: 123456
				mongo connnect by: localhost:9017 user: test ; pass: 123456


# rebuild  prj code to docker image

check file docker-compose.yml with services name: webapiteamplate to find where is dockerfile to build image
                
                docker-compose build webapiteamplate
