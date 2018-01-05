@echo off
cls
title CyNetTools NAnt build
set arg=%1
set buildfile=%CD%\CyNetTools.build
if exist "%buildfile%" (
	nant
	set err=%ERRORLEVEL%
) else (
	echo ERROR: Missing build file: %buildfile%
	set buildfile=
)
pause