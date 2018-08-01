rem @"%MONO_HOME%\bin\mono.exe" %MONO_OPTIONS% "%MONO_HOME%\lib\mono\4.5\pdb2mdb.exe" %1%2.dll

copy %1%2.dll ..\..\Unity\Assets\Dlls /Y
copy %1%2.pdb ..\..\Unity\Assets\Dlls /Y