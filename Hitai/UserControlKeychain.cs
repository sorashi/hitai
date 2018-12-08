using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Hitai.AsymmetricEncryption;

namespace Hitai
{
    public partial class UserControlKeychain : UserControl
    {
        private readonly Dictionary<KeyPair, ListViewItem> _keyPairToListViewItemMap =
            new Dictionary<KeyPair, ListViewItem>();

        private Keychain _keychain;

        public UserControlKeychain() {
            InitializeComponent();
        }

        public KeyPair[] SelectedItems {
            get {
                IEnumerable<KeyPair> keypairs = listView.SelectedItems
                    .Cast<ListViewItem>()
                    .Select(item => _keyPairToListViewItemMap.Keys
                        .FirstOrDefault(x => _keyPairToListViewItemMap[x] == item));
                KeyPair[] enumeration = keypairs as KeyPair[] ?? keypairs.ToArray();
                if (enumeration.Any(x => x == null))
                    throw new Exception("Could not find ListItem-respective keypair");
                return enumeration;
            }
        }

        public Keychain Keychain {
            get => _keychain;
            set {
                _keychain = value;
                RefreshListView();
            }
        }

        private void Keychain_OnKeypairRemoved(KeyPair keypair) {
            _keyPairToListViewItemMap[keypair].Remove();
            _keyPairToListViewItemMap.Remove(keypair);
        }

        private void Keychain_OnKeypairAdded(KeyPair keypair) {
            // private? | id | name | created | expires
            var item = new ListViewItem(new[] {
                keypair.IsPrivate.ToString(),
                keypair.ShortId,
                keypair.UserId,
                keypair.CreationTime.ToShortDateString(),
                keypair.Expires.ToShortDateString()
            });
            _keyPairToListViewItemMap.Add(keypair, item);
            listView.Items.Add(item);
        }

        private void RefreshListView() {
            _keyPairToListViewItemMap.Clear();
            listView.Items.Clear();
            foreach (KeyPair keypair in Keychain.Keys) {
                // private? | id | name | created | expires
                var item = new ListViewItem(new[] {
                    keypair.IsPrivate.ToString(),
                    keypair.ShortId,
                    keypair.UserId,
                    keypair.CreationTime.ToShortDateString(),
                    keypair.Expires.ToShortDateString()
                });
                _keyPairToListViewItemMap.Add(keypair, item);
                listView.Items.Add(item);
            }
        }

        private async void UserControlKeychain_Load(object sender, EventArgs e) {
            Keychain = await Keychain.GetInstance();
            Keychain.OnKeypairAdded += Keychain_OnKeypairAdded;
            Keychain.OnKeypairRemoved += Keychain_OnKeypairRemoved;
        }
    }
}
