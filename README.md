##Command line usage

###Synopsis
**githubdater.exe** **[ -p**=32768 **]** **[ -f**=myApp.exe **]** **[ -u**=custom.manifest  **-t**=github **]**

###Options
`-p`, `--initiating-process-pid`

&nbsp;&nbsp;&nbsp;&nbsp;Specify the pid of the process to kill to be able to update the application files

`-f`, `--follow-up-process-path`

&nbsp;&nbsp;&nbsp;&nbsp;Specify path of the application's executable to launch after the update. This path can be absolute or relative to the **githubdater** executable.

`-u`, `--update-manifest-name`

&nbsp;&nbsp;&nbsp;&nbsp;Specify the update manifest name (with extension) if it's not the default one

`-t`, `--update-manifest-type`

&nbsp;&nbsp;&nbsp;&nbsp;Specify the update manifest type if its name does not follow the default conventions which are:

- `github.update.manifest` for a GitHub manifest
- `gitlab.update.manifest` for a GitLab manifest (not yet supported)

&nbsp;&nbsp;&nbsp;&nbsp;Allowed values for update type are:

- `github`
- `gitlab` (not yet supported)
