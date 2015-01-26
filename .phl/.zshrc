
ZSH_THEME="half-life"
project_plugins=(phl-helper phl_xamarin)

export project_home="$ZDOTDIR/.."
export project_info="$ZDOTDIR/info"

test -f "$OLD_ZDOTDIR/.zshenv" && . "$OLD_ZDOTDIR/.zshenv"
test -f "$OLD_ZDOTDIR/.zshrc"  && . "$OLD_ZDOTDIR/.zshrc"

PS1="%{$fg[cyan]%}(patbonecrusher:MvxAdvancedPresenter.Touch) $PS1" #tell the user he’s in a modified shell

ZDOTDIR=${OLD_ZDOTDIR}
cd ${project_home}
