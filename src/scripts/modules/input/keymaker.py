keys = "a.b.c.d.e.f.g.h.i.j.k.l.m.n.o.p.q.r.s.t.u.v.w.x.y.z.1.2.3.4.5.6.7.8.9.0.f1.f2.f3.f4.f5.f6.f7.f8.f9.f10.f11.f12.escape.backquote.minus.equals.backspace.tab.leftbracket.rightbracket.backslash.capslock.semicolon.quote.return.lshift.comma.period.rshift.lctrl.lalt.ralt.application.rctrl.up.down.left.right"

#for i, item in enumerate(keys.split(".")):
    #if len(item) == 1:
    #    print("case SDL.SDL_Keycode.SDLK_" + item + ":")
    #else:
    #    print("case SDL.SDL_Keycode.SDLK_" + item.upper() + ":")
    #print("    input.pressed_keys[input.key_" + item + "] = false;")
    #print("    break;")
    #print("public static readonly int key_" + item + " = " + str(i) + ";")

#input()

print(str(keys.split('.')).replace("'", '"'))