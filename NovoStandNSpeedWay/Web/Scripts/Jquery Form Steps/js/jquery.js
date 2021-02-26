(function ()
{
    var versionPattern = /^[\?|\&]{1}version=(\d\.\d\.\d|latest)&?$/,
        version = versionPattern.exec(location.search),
        defaultVersion = "3.5.1",
        file = "http://code.jquery.com/jquery-git.js";

    if (version != null && version.length > 0)
    {
        version = version[1];
    }
    else
    {
        version = defaultVersion;
    }

    if (version !== "latest")
    {
        file = "../Scripts/jquery-" + version + ".js";
    }

    document.write("<script type='text/javascript' src='" + file + "'></script>");
})();