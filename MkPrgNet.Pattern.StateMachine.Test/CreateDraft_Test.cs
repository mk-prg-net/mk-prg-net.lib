using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Diagnostics;

namespace MkPrgNet.Pattern.StateMachine.Test
{
    [TestClass]
    public class CreateDraft_Test
    {
        [TestMethod]
        public void StateMachine_CreateDraft_Create()
        {
            var fsm = new CreateDraft.Automat();
            fsm.Start();
            Assert.IsInstanceOfType(fsm.CurrentState, typeof(CreateDraft.Start.State));
        }

        private static void InputProcessOutputCycle(CreateDraft.Automat fsm, Queue<CreateDraft.DefDraftToken> queue)
        {
            while (!fsm.CurrentState.IsFinal)
            {
                // Alle Eingänge einlesen
                var token = queue.Dequeue();
                foreach (CreateDraft.InputBase input in fsm.Inputs)
                {
                    input.TryRead(token);
                }

                fsm.Transistion();
            }
        }

        [TestMethod]
        public void StateMachine_CreateDraft_Doc()
        {
            var fsm = new CreateDraft.Automat();
            fsm.Start();

            var queue = new Queue<CreateDraft.DefDraftToken>();

            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.Author, "mko"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.Node, "n1"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.Type, "doc"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.DocTheme, "Automatentheorie"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.DocToc, "mko.n1.informatik"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.seal, ""));

            fsm.DefineOutputFunc(fsm.GetInputIx("InputTheme"), fsm.GetStateIx("DocDefTheme"), input => Debug.WriteLine("Ein Dokument zum Thema " + ((CreateDraft.DocDefTheme.InputTheme)input).Token.TokenContent + " wird angelegt"));

            InputProcessOutputCycle(fsm, queue);

            Assert.IsInstanceOfType(fsm.CurrentState, typeof(CreateDraft.Seal.State));
        }


        [TestMethod]
        public void StateMachine_CreateDraft_DocErr1()
        {
            var fsm = new CreateDraft.Automat();
            fsm.Start();

            var queue = new Queue<CreateDraft.DefDraftToken>();

            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.Author, "mko"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.Node, "n1"));
            //queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.Type, "doc"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.DocTheme, "Automatentheorie"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.DocToc, "mko.n1.informatik"));
            queue.Enqueue(new CreateDraft.DefDraftToken(CreateDraft.DefDraftToken.EnumTokenType.seal, ""));

            InputProcessOutputCycle(fsm, queue);

            Assert.IsInstanceOfType(fsm.CurrentState, typeof(CreateDraft.Error.State));
        }


    }
}
