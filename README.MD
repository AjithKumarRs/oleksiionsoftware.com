# oleksiionsoftware.com

## Intro

## Tips

#### Add local .bashrc to global .bashrc

```
echo "PROMPT_COMMAND='if [[ \"$bashrc\" != \"$PWD\" && \"$PWD\" != \"$HOME\" && -e .bashrc ]]; then bashrc=\"$PWD\"; . .bashrc; fi'" >> ~/.bashrc
```