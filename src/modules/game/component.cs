using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;
using Fjord.Modules.Camera;
using System.Collections.Generic;
using System;

namespace Fjord.Modules.Game {
    public abstract class component {
        public dynamic parent;

        public virtual void on_load() {}
        public virtual void update() {}
        public virtual void render() {}

        public static entity entity_place(V2 pos) {
            List<entity> entities = scene_handler.get_current_scene().get_entities();

            foreach(entity e in entities) {
                Sprite_Renderer sprite = e.get<Sprite_Renderer>();
                V2 origin = sprite.sprite.get_origin();
                if(helpers.point_inside(pos, new V4(pos.x - origin.x, pos.y - origin.y, sprite.sprite.get_draw_size().x, sprite.sprite.get_draw_size().y))) {
                    return e;
                }
            }
            return null;
        }

        public static entity entity_place(V2 pos, Type t) {
            List<entity> entities = scene_handler.get_current_scene().get_entities();

            foreach(entity e in entities) {
                Sprite_Renderer sprite = e.get<Sprite_Renderer>();
                V2 origin = sprite.sprite.get_origin();
                Type t2 = e.GetType();
                if(helpers.point_inside(pos, new V4(pos.x - origin.x, pos.y - origin.y, sprite.sprite.get_draw_size().x, sprite.sprite.get_draw_size().y))) {
                    if(t.Equals(e)) {
                        return e;
                    }
                }
            }
            return null;
        }
    }

    public class Transform : component {
        public V2f position = new V2f(0, 0);
        public V2f scale = new V2f(1, 1);
        public float rotation = 0f;
    }

    public class Sprite_Renderer : component {
        public texture sprite = new texture();

        public bool visible = true;

        public override void update()
        {
            sprite.set_scale(parent.get<Transform>().scale);
            sprite.set_angle(parent.get<Transform>().rotation);
        }

        public override void render()
        {
            if(visible)
                draw.texture_direct(parent.get<Transform>().position - camera.get(), sprite);
        }
    }

    public class Rigidbody : component {
        Sprite_Renderer sprite;
        Transform transform;

        string collide_with = "collision";
        dynamic collide_check = true;

        public float gravity = 0.04f;
        public float max_fall_speed = 20f;
        public V2f velocity = new V2();

        public bool collide_right = false;
        public bool collide_left = false;
        public bool collide_down = false;
        public bool collide_up = false;

        public void collision(string collide_id, dynamic collide_check) {
            this.collide_with = collide_id;
            this.collide_check = collide_check;
        }

        public override void on_load()
        {
            sprite = parent.get<Sprite_Renderer>();
            sprite.sprite.set_origin(draw_origin.BOTTOM_MIDDLE);
            transform = parent.get<Transform>();
            
            base.on_load();
        }

        public override void update()
        {   
            #nullable enable
            collide_down = collide_left = collide_right = collide_up = false;

            velocity.y += gravity;
            if(velocity.y > max_fall_speed) {
                velocity.y = max_fall_speed;
            }

            for(var i = 0; i < sprite.sprite.get_draw_size().x; i++) {
                int velyoffset = velocity.y < -1 ? -1 : 1; 
                if(scene_handler.get_tile(new V2((int)(transform.position.x - sprite.sprite.get_draw_size().x / 2 + i),  (int)(transform.position.y + velyoffset)))[collide_with] == collide_check) {
                    collide_down = true;
                }
            }

            if(collide_down) {
                velocity.y = 0;
            }

            for(var i = 0; i < sprite.sprite.get_draw_size().x; i++) {
                int velyoffset = velocity.y < -1 ? 0 : 1; 
                if(scene_handler.get_tile(new V2((int)(transform.position.x - sprite.sprite.get_draw_size().x / 2 + i),  (int)(transform.position.y - sprite.sprite.get_draw_size().y + velyoffset)))[collide_with] == collide_check) {
                    collide_up = true;
                }
            }

            if(collide_up) {
                velocity.y = 0;
            }

            for(var i = 0; i < sprite.sprite.get_draw_size().y; i++) {
                int velxoffset = velocity.x > 0.01f ? 2 : -1;
                if(scene_handler.get_tile(new V2((int)(transform.position.x + sprite.sprite.get_draw_size().x / 2 + velxoffset),  (int)(transform.position.y - sprite.sprite.get_draw_size().y + i)))[collide_with] == collide_check) {
                    collide_right = true;
                }
            }

            if(collide_right) {
                velocity.x = 0;
            }

            for(var i = 0; i < sprite.sprite.get_draw_size().y - 1; i++) {
                int velxoffset = velocity.x < -0.01f ? -1 : 1;
                if(scene_handler.get_tile(new V2((int)(transform.position.x - sprite.sprite.get_draw_size().x / 2 + velxoffset),  (int)(transform.position.y - sprite.sprite.get_draw_size().y + i)))[collide_with] == collide_check)  {
                    collide_left = true;
                }
            }

            if(collide_left) {
                velocity.x = 0;
            }
            
            transform.position.y += velocity.y * (float)game.delta_time;
            transform.position.x += velocity.x;

            base.update();
        }

        public override void render()
        {
            // draw.rect(new V4(, sprite.sprite.get_draw_size().x, 1), color.green);

            // for(var i = 0; i < sprite.sprite.get_draw_size().x; i++) {
            //     int velyoffset = velocity.y < -1 ? -1 : 1; 
            //     draw.rect(new V4((int)(transform.position.x - sprite.sprite.get_draw_size().x / 2 - camera.get().x + i),  (int)(transform.position.y + velyoffset - camera.get().y), 1, 1), color.green);
            // }

            // for(var i = 0; i < sprite.sprite.get_draw_size().x; i++) {
            //     int velyoffset = velocity.y < -1 ? -1 : 1; 
            //     draw.rect(new V4((int)(transform.position.x - sprite.sprite.get_draw_size().x / 2 - camera.get().x + i),  (int)(transform.position.y - sprite.sprite.get_draw_size().y + velyoffset - camera.get().y), 1, 1), color.green);
            // }

            // for(var i = 0; i < sprite.sprite.get_draw_size().y; i++) {
            //     // int velyoffset = velocity.y < -1 ? -1 : 1; 
            //     int velxoffset = velocity.x > 0.01f ? 2 : -1;
            //     draw.rect(new V4((int)(transform.position.x + sprite.sprite.get_draw_size().x / 2 + velxoffset - camera.get().x),  (int)(transform.position.y - sprite.sprite.get_draw_size().y - camera.get().y + i), 1, 1), color.green);
            // }

            // for(var i = 0; i < sprite.sprite.get_draw_size().y; i++) {
            //     // int velyoffset = velocity.y < -1 ? -1 : 1; 
            //     int velxoffset = velocity.x < -0.01f ? -1 : 1;
            //     draw.rect(new V4((int)(transform.position.x - sprite.sprite.get_draw_size().x / 2 + velxoffset - camera.get().x),  (int)(transform.position.y - sprite.sprite.get_draw_size().y - camera.get().y + i), 1, 1), color.green);
            // }
            // draw.rect(new V4((int)(transform.position.x - sprite.sprite.get_draw_size().x / 2 - camera.get().x), (int)(transform.position.y - sprite.sprite.get_draw_size().y - camera.get().y), sprite.sprite.get_draw_size().x, sprite.sprite.get_draw_size().y), color.black);


            base.render();
        }
    }
}