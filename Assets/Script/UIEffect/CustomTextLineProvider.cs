using System.Collections.Generic;

#if USE_ADDRESSABLES
using UnityEngine.ResourceManagement.AsyncOperations;
#endif

namespace Yarn.Unity
{
    public class CustomTextLineProvider : LineProviderBehaviour
    {
        /// <summary>Specifies the language code to use for text content
        /// for this <see cref="TextLineProvider"/>.


        public override LocalizedLine GetLocalizedLine(Yarn.Line line)
        {
            var text = YarnProject.GetLocalization(GameManager.LanguageCode).GetLocalizedString(line.ID);
            return new LocalizedLine()
            {
                TextID = line.ID,
                RawText = text,
                Substitutions = line.Substitutions,
                Metadata = YarnProject.lineMetadata.GetMetadata(line.ID),
            };
        }

        public override void PrepareForLines(IEnumerable<string> lineIDs)
        {
            // No-op; text lines are always available
        }

        public override bool LinesAvailable => true;

        public override string LocaleCode => GameManager.LanguageCode;
    }
}

