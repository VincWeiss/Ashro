# ASHRO

## A project using the MS Hololens and Unity
Ziel des Spiels ist es die laufenden Kosten, die durch den Betrieb des Spargel-Ernte-Roboters entstehen, 
mit dem Spargelverkauf zu decken. Der Spieler soll so schnell und so viele Spargel wie möglich zum Lager bringen, 
damit diese dann verkauft werden können. Die Steuerung funktioniert dabei über die HoloLens-Gesten, mit welchen der 
User die Spargelfelder auswählen kann, die vom Roboter als nächstes angefahren werden sollen.

### Developing Vincent Weiss, Oxana Doroshkevich, Marco Himmelstein, Selina Magnin, Frederick Wurfer

## Tools

1. Visual Studio 2015 mit Update 3
2. Unity

## Prerequisites
- 64 bit Windows 10 Education, Enterprise oder Pro für die Hyper-V Unterstützung
- 64-bit CPU
- 4-Kerne-CPU
- mind. 8 GB RAM
- GPU (DirectX 11.0 or later)

### Installing

fork this reposototy to your own account and switch to the forked one
now in your new "own" forked reposotory click on the green "clone or download" button and download the zip file
1. open git bash
2. in git bash: switch to the directory you have chosen for the downloaded and hopefully now unzipped project file
3. in git bash: git init
4. git remote add orinin https://github.com/YOURREPOSOTORYNAMEHERE/Ashro.git
5. git remote add SOMENAMEOFAREPOHERE https://github.com/VincWeiss/Ashro.git
6. check your reposotory status with: git remote -v
this should allow you now to push to your own master and pull from the master master ;)

if you want to commit some changes, follow the following instruction:

1. git status
2. git add . (for all files that were changed) OR git add filename (for specific file)
3. git commit -m "SOME MESSAGE HERE"
4. git push origin master
I would recommend to do the pull requests to merge the local code to the master master branch. so that everything is alwas save from merging error!

Good to know. If you have some problems with git and you can't pull or push or even checkout the master again or something and you're just fucking stuck...

git fetch --all
git reset --hard origin/master

to get the version of your accounts master branch status.

### And coding style

``` The Car Engine
private void ApplySteer() {
        if (patrol.userNodes.Count > 0) {
            Vector3 relativeVector = transform.InverseTransformPoint(patrol.userNodes[0].transform.position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;
        } else {
            Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;
        }
    }
```

### Built With

1. https://www.microsoft.com/de-de/hololens
2. https://unity3d.com/de/learn/tutorials/topics/virtual-reality/user-interfaces-vr
3. https://www.visualstudio.com/de/downloads/

## Versioning

Version 0.0.1

## Authors

* **Vincent Weiss** - *Initial work* - [Vincent Weiss](https://github.com/VincWeiss)
* **Oxana Doroshkevich** - *Design work* - [Oxana Doroshkevich](https://github.com/OxanaDoroshkevich)
* **Selina Magnin** - *Project Manager* - [Selina Magnin](https://github.com/SelToTheIna)
* **Marco Himmelstein** - *Developer* - [Marco Himmelstein](https://github.com/himmelst94)
* **Frederick Wurfer** - *Developer* - [Frederick Wurfer](https://github.com/FreddyWurfer)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Cooperation with the university of Reutlinegn and UID
* Inspiration: UID (User Interface Design GmbH) http://www.uid.com/
* Working with Jannik Lassahn 
