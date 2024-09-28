namespace Hearings.SaveSystem
{
    [System.Serializable]
    public struct CharacterData
    {
        public int spawnObjectId;
        public int positionOnBoard;

        public CharacterData(int spawnObjectId, int positionOnBoard)
        {
            this.spawnObjectId = spawnObjectId;
            this.positionOnBoard = positionOnBoard;
        }
    }
}