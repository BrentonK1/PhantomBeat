namespace PhantomBeat {
    public static class TagGenerator {

        /// <summary> `BuildFromDirection` builds a tag by concatenating some prefix with a direction.  </summary>
        /// <param name="prefix"> The prefix of the tag. </param>
        /// <param name="direction"> The direction used in the creation of this tag. </param>
        public static string BuildFromDirection(string prefix, Direction direction) {
            return prefix + direction.ToString();
        }
    }

}
