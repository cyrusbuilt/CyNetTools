@echo off
cls
title CyNetTools NAnt build
set arg=%1

:: Get script directory
set SCRIPT_DIR=%~dp0
set SCRIPT_DIR=%SCRIPT_DIR:~0,-1%

:: Get NAnt exec
set NANT=%SCRIPT_DIR%\packages\NAnt.0.92.0\tools\nant.exe
set buildfile=%SCRIPT_DIR%\CyNetTools.build

:: Run the build.
if exist "%buildfile%" (
	if exist "%NANT%" (
		%NANT% %arg%
		set err=%ERRORLEVEL%
	) else (
		echo ERROR: Could not find NAnt executable: %NANT%
		echo ERROR: Is nuget packaget installed?
	)
) else (
	echo ERROR: Missing build file: %buildfile%
	
)

set buildfile=
set arg=
set SCRIPT_DIR=
set NANT=
set err=