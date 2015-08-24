<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<title>Unity Web Player | CYounity</title>
		<script type='text/javascript' src='https://ssl-webplayer.unity3d.com/download_webplayer-3.x/3.0/uo/jquery.min.js'></script>
		<script type="text/javascript">
		<!--
		var unityObjectUrl = "http://webplayer.unity3d.com/download_webplayer-3.x/3.0/uo/UnityObject2.js";
		if (document.location.protocol == 'https:')
			unityObjectUrl = unityObjectUrl.replace("http://", "https://ssl-");
		document.write('<script type="text\/javascript" src="' + unityObjectUrl + '"><\/script>');
		-->
		</script>
        <script type='text/javascript' src='unity.js' ></script>
        <link rel='stylesheet' type='text/css' href='unity.css' ></script>
	</head>
	<body>
		<p class="header"><span>Unity Web Player | </span>CYounity</p>
		<div class="content">
			<div id="unityPlayer">
				<div class="missing">
					<a href="http://unity3d.com/webplayer/" title="Unity Web Player. Install now!">
						<img alt="Unity Web Player. Install now!" src="http://webplayer.unity3d.com/installation/getunity.png" width="193" height="63" />
					</a>
				</div>
				<div class="broken">
					<a href="http://unity3d.com/webplayer/" title="Unity Web Player. Install now! Restart your browser after install.">
						<img alt="Unity Web Player. Install now! Restart your browser after install." src="http://webplayer.unity3d.com/installation/getunityrestart.png" width="193" height="63" />
					</a>
				</div>
			</div>
		</div>
        <script type="text/javascript" language="javascript">
            var u = new UnityObject2();
            u.initPlugin(jQuery("#unityPlayer")[0], "Example.unity3d");
            function Loaded( arg )
            {
                
<?php
if (!isset($_GET['maze'])) {
    echo 'u.getUnity().SendMessage("WebManager", "GiveError", "No game file selected, you butt");';
    //exit('no gamefile');
} else {
    echo 'u.getUnity().SendMessage("WebManager", "LoadLevel", "' . $_GET['maze'] . '");';
}

?>
            }
        </script>
	</body>
</html>
