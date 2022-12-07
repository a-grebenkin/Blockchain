using System;
using System.Text;
using System.Security.Cryptography;

namespace blockchain
{
	class Node
	{
		private string text;
		private byte[] prev_hash;
		private int nonce;
		private DateTime timeCreate;

        public byte[] Hash { get { return ComputeHash(nonce, prev_hash, text); } }
		public void ChangeHash()
		{
			prev_hash[0] += 1;
		}
        public Node(string text, byte[] hash = null)
		{
			this.text = text;
			this.prev_hash = hash;
            timeCreate = DateTime.Now;
			nonce = 0;
			while (ComputeHash(nonce, prev_hash, text)[0]!=0)
			{
				nonce++;
			}
        }


        private byte[] ComputeHash(int nonce, byte[] prev_hash, string text)
		{
			SHA256 sha = SHA256.Create(); 
			byte[] textBytes = Encoding.Default.GetBytes(prev_hash + text+ nonce); 

			return sha.ComputeHash(textBytes); 
		}

		public string Text { get { return this.text; } }

        public string TimeCreate { get { return timeCreate.ToString(); } }

        public int Nonce { get { return nonce; } }
        public string PreviousHashString
		{
			get
			{
				if (this.prev_hash == null)
					return "[Отсутствует]";
				return Encoding.Default.GetString(prev_hash);
			}
		}

		public string HashString
		{
			get
			{
				return Encoding.Default.GetString(Hash);
			}
		}

	}
}