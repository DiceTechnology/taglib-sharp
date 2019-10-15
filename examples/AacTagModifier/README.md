# AAC ID3 Tag Modifier
Dice Technology AAC file ID3 tag modifier.

## Build the application
To build the project run the following command from the root directory of the `taglib-sharp` solution:

		msbuild examples/AacTagModifier/AacTagModifier.csproj

The results of the build can be found in the `examples/AacTagModifier/bin/Debug/net45/` folder.

## Deploy a new version of the application
To deploy the application you need to copy the relevant files to S3. The relevant files are found in the `examples/AacTagModifier/bin/Debug/net45/` folder.
They are:

		AacTagModifier.exe
		TagLibSharp.dll

Upload these files into a new version folder in S3. The base directory is `s3://dge-artifacts/technology/dice/std/cattle/taglib-sharp/`. 
Check the latest version folder in there and increment by 1 (e.g. `/v1.0.5` -> `/v1.0.6`). Upload both files to this directory.

		aws s3 cp --recursive examples/AacTagModifier/bin/Debug/net45/ s3://dge-artifacts/technology/dice/std/cattle/taglib-sharp/{VERSION}/

## Run the application locally
To run the application use the following command:

		mono bin/Debug/net45/AacTagModifier.exe [AAC_FILE_LOCATION] [TIMESTAMP]

Where [AAC_FILE_LOCATION] is the location of the AAC file which you wish to modify the ID3 tag of. 
And [TIMESTAMP] is the start PTS of the AAC file as a long.

You can also run the application through Visual Studio with a custom run configuration.
