param (
	[switch] $fetch = $FALSE,
	[string] $tag = "trunk"
)

if ($fetch) {
	"Fetching trunk from $tag from svn.castleproject.org..."
	$url = "http://svn.castleproject.org:8080/svn/castle"
	if ($tag -eq "trunk") {
		$url = "$url/$tag"
	} else {
		$url = "$url/tags/$tag"
	}
		
	svn export $url
}

if (!$(Test-Path $tag)) {
	Throw "I expected to see a path called $tag but there isn't one."
}

Push-Location $tag

..\..\Nant\NAnt.exe `
	'-D:build.debug=PdbOnly' `
	quick `
	release `
	rebuild

Pop-Location

Remove-Item *.dll
Remove-Item *.pdb
Remove-Item *.exe

Copy-Item  -Recurse -Force "$tag\build\net-3.5\release\*" .

@"

Done.

$tag Castle DLLs and PDBs are in this folder.

"@