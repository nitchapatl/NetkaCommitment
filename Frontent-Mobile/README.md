# Netka Commitment Mobile 2020

## How to build android and ios application.

You must install nodejs first.

Quick installation

```
npm install -g cordova
Go to projet root directory
npm install (Discovery libary dependencies)
cordova prepare (Discovery libary dependencies)

Android (Ensure unknown sources is checked if deploy to your developer devices)
cordova build android
cordova run android

iOS (Ensure trust sign cretificate if deploy to your developer devices)
cordova build ios
cordova run ios

```

## Dependencies
```
cordova 9.0.0

npm install -g cordova@9.0.0
Go to projet root directory

Plugins

cordova plugin add cordova-plugin-whitelist --save
cordova plugin add cordova-plugin-camera --save
cordova plugin add cordova-plugin-compat --save
cordova plugin add cordova-plugin-device --save
cordova plugin add cordova-plugin-file --save
cordova plugin add cordova-plugin-file-transfer --save
cordova plugin add cordova-plugin-geolocation --save

Build icons

Mac\Linux\Unix : brew install imagemagick 
Windows : http://www.imagemagick.org/script/binary-releases.php#windows (check "Legacy tools") 
npm install cordova-icon -g
Example
cordova-icon --config=config.xml --icon=icon.png (You also can specify manually a location for your config.xml or icon.png)
cordova-icon --xcode-old (If you run a old version of Cordova for iOS and you need your files in /Resources/, use this option)

Splash screen

Mac\Linux\Unix : brew install imagemagick 
Windows : http://www.imagemagick.org/script/binary-releases.php#windows (check "Legacy tools") 
npm install cordova-splash -g
Example
 - cordova-splash --config=config.xml --splash=splash.png (You also can specify manually a location for your config.xml or splash.png)
 - cordova-splash --xcode-old (If you run a old version of Cordova for iOS and you need your files in /Resources/, use this option)

```

### Android
```
cordova platform add android --save
cordova requirements android (Checking dependencies)
cordova build android
cordova run android
```


### iOS (Require Xcode libaries)
```
cordova platform add ios --save
cordova requirements ios (Checking dependencies)
cordova build ios
cordova run ios
```
