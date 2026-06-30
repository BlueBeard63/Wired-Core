using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wired.WiredAssets;
using Wired.Utilities;

namespace Wired
{
    public class Resources
    {
        public EffectAsset node_consumer;
        public EffectAsset node_consumer_selected;
        public EffectAsset path_consumer;
        public EffectAsset preview_consumer;

        public EffectAsset node_power;
        public EffectAsset node_power_selected;
        public EffectAsset path_power;
        public EffectAsset preview_power;

        public EffectAsset node_gate;
        public EffectAsset node_gate_selected;
        public EffectAsset path_gate;
        public EffectAsset preview_gate;

        public EffectAsset node_timer;
        public EffectAsset node_timer_selected;
        public EffectAsset path_timer;
        public EffectAsset preview_timer;

        public EffectAsset node_subnode;
        public EffectAsset node_subnode_selected;
        public EffectAsset path_subnode;
        public EffectAsset preview_subnode;

        public EffectAsset wire_2m;
        public EffectAsset wire_4m;
        public EffectAsset wire_6m;
        public EffectAsset wire_8m;

        public EffectAsset goggles_ui;

        public ItemBarricadeAsset generator_technical;

        public List<Guid> nodeeffects = new List<Guid>();
        public List<Guid> previeweffects = new List<Guid>();
        public const short GogglesUIKey = 15302;

        public Resources()
        {
            node_consumer = (EffectAsset)Assets.find(new Guid("ad1529d6692f473ead2ac79e70e273fb"));
            node_consumer_selected = (EffectAsset)Assets.find(new Guid("243dd59b120645aaa44094be92f0576a"));
            path_consumer = (EffectAsset)Assets.find(new Guid("0c3f255bcdb94ae0867de0c7de4d0f3e"));
            preview_consumer = (EffectAsset)Assets.find(new Guid("ff71a083b21b433aa4215ce0ad79c96c"));

            node_power = (EffectAsset)Assets.find(new Guid("f9f8409f96fe4624a280181523e5966d"));
            node_power_selected = (EffectAsset)Assets.find(new Guid("ada554f88d6c4fc88ffa9837ed426971"));
            path_power = (EffectAsset)Assets.find(new Guid("aa4bf9e416b248f8b6ae4e48b30382a7"));
            preview_power = (EffectAsset)Assets.find(new Guid("dc925c65d83842bfb20ddae50ff71093"));

            node_gate = (EffectAsset)Assets.find(new Guid("8b7f38d937a0403eb99e97b535d9df83"));
            node_gate_selected = (EffectAsset)Assets.find(new Guid("2042df5fec3346e89974a595665fa4b5"));
            path_gate = (EffectAsset)Assets.find(new Guid("f8858fb1a24c4d70b1ca9a15e70606d5"));
            preview_gate = (EffectAsset)Assets.find(new Guid("b86d7db716914a199a82b7e35931840e"));

            node_timer = (EffectAsset)Assets.find(new Guid("510beb5970b94441a248baed6e0d172d"));
            node_timer_selected = (EffectAsset)Assets.find(new Guid("9143acaa55904b28b8a6310e26ba0437"));
            path_timer = (EffectAsset)Assets.find(new Guid("e0377b8351c945a797350447bd513a1e"));
            preview_timer = (EffectAsset)Assets.find(new Guid("d9eac6e465944769b37a3cc8f605a499"));

            node_subnode = (EffectAsset)Assets.find(new Guid("938c3fc3216d487cb71475f90ae603e1"));
            node_subnode_selected = (EffectAsset)Assets.find(new Guid("e4715014f5db48aba52ee7c4ab74f8d0"));
            path_subnode = (EffectAsset)Assets.find(new Guid("58a4907b17bc4b3785b468adf0346bff"));
            preview_subnode = (EffectAsset)Assets.find(new Guid("d9eac6e465944769b37a3cc8f605a499"));

            goggles_ui = (EffectAsset)Assets.find(new Guid("1b826f8e3be1454384a6130f3298ddf9"));

            wire_2m = (EffectAsset)Assets.find(new Guid("d29b911b2d1a4b4f9100c24e6d08b125"));
            wire_4m = (EffectAsset)Assets.find(new Guid("2581cc5b12df417f9cd53c3bdd8cf764"));
            wire_6m = (EffectAsset)Assets.find(new Guid("cabc431d23c64693832dcfaea438a219"));
            wire_8m = (EffectAsset)Assets.find(new Guid("4548122b04b74bcba1e59186f68683df"));

            generator_technical = (ItemBarricadeAsset)Assets.find(new Guid("101d13181ef1407ca583686f36663a0f"));

            nodeeffects.Add(node_consumer.GUID);
            nodeeffects.Add(node_power.GUID);
            nodeeffects.Add(node_gate.GUID);
            nodeeffects.Add(node_timer.GUID);
            nodeeffects.Add(node_subnode.GUID);
            nodeeffects.Add(path_consumer.GUID);
            nodeeffects.Add(path_power.GUID);
            nodeeffects.Add(path_gate.GUID);
            nodeeffects.Add(path_timer.GUID);
            nodeeffects.Add(path_subnode.GUID);

            previeweffects.Add(node_consumer_selected.GUID);
            previeweffects.Add(node_power_selected.GUID);
            previeweffects.Add(node_gate_selected.GUID);
            previeweffects.Add(node_timer_selected.GUID);
            previeweffects.Add(node_subnode_selected.GUID);
            previeweffects.Add(preview_consumer.GUID);
            previeweffects.Add(preview_power.GUID);
            previeweffects.Add(preview_gate.GUID);
            previeweffects.Add(preview_timer.GUID);
        }
    }
}
