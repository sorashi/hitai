using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hitai.AsymmetricEncryption;

namespace Hitai
{
    public partial class UserControlKeychain : UserControl
    {
        public UserControlKeychain() {
            InitializeComponent();
        }
        public KeyPair[] SelectedItems {
            get {
                var keypairs = listView.SelectedItems
                    .Cast<ListViewItem>()
                    .Select(item => _keyPairToListViewItemMap.Keys
                        .Where(x => _keyPairToListViewItemMap[x] == item)
                        .FirstOrDefault());
                if (keypairs.Any(x => x == null)) throw new Exception("Could not find listitem-respective keypair");
                return keypairs.ToArray();
            }
        }
        private void Keychain_OnKeypairRemoved(KeyPair keypair) {
            _keyPairToListViewItemMap[keypair].Remove();
            _keyPairToListViewItemMap.Remove(keypair);
        }
        private Dictionary<KeyPair, ListViewItem> _keyPairToListViewItemMap = new Dictionary<KeyPair, ListViewItem>();
        private void Keychain_OnKeypairAdded(KeyPair keypair) {
            // private? | id | name | created | expires
            var item = new ListViewItem(new string[] {
                keypair.IsPrivate.ToString(),
                keypair.ShortId,
                keypair.UserId,
                keypair.CreationTime.ToShortDateString(),
                keypair.Expires.ToShortDateString()
            });
            _keyPairToListViewItemMap.Add(keypair, item);
            this.listView.Items.Add(item);
        }
        private void RefreshListView() {
            _keyPairToListViewItemMap.Clear();
            listView.Items.Clear();
            foreach(var keypair in Keychain.Keys) {
                // private? | id | name | created | expires
                var item = new ListViewItem(new string[] {
                keypair.IsPrivate.ToString(),
                keypair.ShortId,
                keypair.UserId,
                keypair.CreationTime.ToShortDateString(),
                keypair.Expires.ToShortDateString()
            });
                _keyPairToListViewItemMap.Add(keypair, item);
                this.listView.Items.Add(item);
            }
        }
        private Keychain _keychain;
        public Keychain Keychain {
            get => _keychain;
            set {
                _keychain = value;
                RefreshListView();
            }
        }

        private async void UserControlKeychain_Load(object sender, EventArgs e) {
            Keychain = await Keychain.GetInstance();
            Keychain.OnKeypairAdded += Keychain_OnKeypairAdded;
            Keychain.OnKeypairRemoved += Keychain_OnKeypairRemoved;
        }
    }
}
