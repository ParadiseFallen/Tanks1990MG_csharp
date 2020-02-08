using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Realisations.Signatures
{
    //Класс для работы с сигнатурой компонентов
    public class ComponentsSignature : IEquatable<ComponentsSignature>
    {
        //Компонентная сигнатура
        public List<Type> Signature { get; set; } = new List<Type>();
        //создать сигнатруру
        public ComponentsSignature(params Type[] componentTypeSignature)
        {
            foreach (var item in componentTypeSignature)
            {
                Signature.Add(item);
            }
        }
        //создать сигнатруру
        public ComponentsSignature(List<IEntityComponent<IEntity>> Signature)
        {
            Signature.ForEach(
                i => { this.Signature.Add(i.GetType()); }
                );
        }
        //создать сигнатруру
        public ComponentsSignature(List<Type> Signature)
        {
            this.Signature = Signature;
        }
        //создать сигнатруру 
        private ComponentsSignature(List<Type> SignatureA, List<Type> SignatureB, bool Add)
        {
            if (Add)
            {
                Signature.AddRange(SignatureA);
                Signature.AddRange(SignatureB);
            }
            else
            {
                Signature = SignatureA.Except(SignatureB).ToList();
            }
        }
        //эквивалентны ли сигнатруы
        public bool Equals(ComponentsSignature other)
        {
            foreach (var item in other.Signature)
            {
                if (!this.Signature.Contains(item))
                    return false;
            }
            return true;
        }
        public static bool operator !=(ComponentsSignature left, ComponentsSignature right) => !left.Equals(right);
        public static bool operator ==(ComponentsSignature left, ComponentsSignature right) => left.Equals(right);
        public static ComponentsSignature operator +(ComponentsSignature left, ComponentsSignature right) => new ComponentsSignature(left.Signature, right.Signature, true);
        public static ComponentsSignature operator -(ComponentsSignature left, ComponentsSignature right) => new ComponentsSignature(left.Signature, right.Signature, false);

    }
}
