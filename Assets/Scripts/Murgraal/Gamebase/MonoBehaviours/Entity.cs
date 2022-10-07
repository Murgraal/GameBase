using UnityEngine;

namespace Murgraal.Gamebase
{
    public abstract class Entity : MonoBehaviour
    {
        private CombatData _combatData = new CombatData();
        private LocationData _locationData = new LocationData();
        private int _instanceID;
    
        public void Awake()
        {
            _instanceID = gameObject.GetInstanceID();
            GameSession.AddEntityData(_instanceID,_combatData,_locationData);
        }

        public void Start()
        {
            Init();
        }

        private void Update()
        {
            if (!GameSession.ShouldUpdateEntities) return;
            Execute();
        }

        private void FixedUpdate()
        {
            if (!GameSession.ShouldUpdateEntities) return;
            FixedExecute();
        }

        public void LateUpdate()
        {
            if (!GameSession.ShouldUpdateEntities) return;
            LateExecute();
            DataUpdate();
        }

        protected virtual void Init() {}
        protected virtual void Execute() {}
        protected virtual void FixedExecute() {}
        protected virtual void LateExecute() {}

        protected void UpdateCombatData(CombatData data)
        {
            _combatData = data;
            GameSession.UpdateCombatData(_instanceID,_combatData);
        }

        private void DataUpdate()
        {
            _locationData.Position = transform.position;
            _locationData.Rotation = transform.rotation;
            GameSession.UpdateLocationData(_instanceID,_locationData);
        }
    }
}